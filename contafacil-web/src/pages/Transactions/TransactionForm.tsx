import { useEffect, useState } from "react";
import { createTransaction } from "../../api/transactions.api";
import { getPeople } from "../../api/people.api";
import { getCategories } from "../../api/categories.api";
import type { Person } from "../../models/Person";
import type { Category } from "../../models/Category";
import { TransactionType } from "../../enums/TransactionType";

interface Props {
  onCreated: () => void;
}

export default function TransactionForm({ onCreated }: Props) {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<string>("");
  const [tipoId, setTipoId] = useState<TransactionType>(TransactionType.Despesa);
  const [pessoaId, setPessoaId] = useState("");
  const [categoryId, setCategoryId] = useState<number | "">("");

  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getPeople().then(setPeople);
    getCategories().then(setCategories);
  }, []);

  const parseCurrencyInput = (raw: string) => {
    const cleaned = raw.trim().replace(/\s/g, "");
    if (!cleaned) return NaN;

    // pt-BR: "." como milhar e "," como decimal (ex.: 1.234,56)
    if (cleaned.includes(",") && cleaned.includes(".")) {
      const normalized = cleaned.replace(/\./g, "").replace(",", ".");
      return Number(normalized);
    }

    // pt-BR: decimal com "," (ex.: 20,00)
    if (cleaned.includes(",")) {
      const normalized = cleaned.replace(",", ".");
      return Number(normalized);
    }

    // fallback: decimal com "." (ex.: 20.00)
    return Number(cleaned);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const valorNumero = parseCurrencyInput(valor);

    if (!descricao || valorNumero <= 0 || !pessoaId || categoryId === "") {
      alert("Preencha todos os campos corretamente");
      return;
    }

    setLoading(true);

    try {
      await createTransaction({
        descricao,
        valor: valorNumero,
        tipoId,
        pessoaId,
        categoryId
      });

      setDescricao("");
      setValor("");
      setTipoId(TransactionType.Despesa);
      setPessoaId("");
      setCategoryId("");

      onCreated();
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Nova Transação</h2>

      <div className="form-grid">
        <div className="form-field">
          <label>Descrição</label>
          <input
            type="text"
            placeholder="Ex.: Salário, aluguel, alimentação..."
            value={descricao}
            onChange={e => setDescricao(e.target.value)}
          />
        </div>

        <div className="form-field">
          <label>Valor</label>
          <input
            type="text"
            inputMode="decimal"
            placeholder="Ex.: 20,00"
            value={valor}
            onChange={e => setValor(e.target.value)}
          />
        </div>

        <div className="form-field">
          <label>Tipo</label>
          <select
            value={tipoId}
            onChange={e => setTipoId(Number(e.target.value) as TransactionType)}
          >
            <option value={TransactionType.Despesa}>Despesa</option>
            <option value={TransactionType.Receita}>Receita</option>
          </select>
        </div>

        <div className="form-field">
          <label>Pessoa</label>
          <select value={pessoaId} onChange={e => setPessoaId(e.target.value)}>
            <option value="">Selecione uma pessoa</option>
            {people.map(p => (
              <option key={p.id} value={p.id}>
                {p.nome}
              </option>
            ))}
          </select>
        </div>

        <div className="form-field">
          <label>Categoria</label>
          <select
            value={categoryId}
            onChange={e => setCategoryId(Number(e.target.value))}
          >
            <option value="">Selecione uma categoria</option>
            {categories.map(c => (
              <option key={c.id} value={c.id}>
                {c.descricao}
              </option>
            ))}
          </select>
        </div>
      </div>

      <button type="submit" className="btn btn-primary" disabled={loading}>
        {loading ? "Salvando..." : "Salvar transação"}
      </button>
    </form>
  );
}
