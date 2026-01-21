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

  const selectedPerson = people.find(p => p.id === pessoaId);
  const isMinor = selectedPerson && selectedPerson.idade < 18;

  useEffect(() => {
    getPeople().then(setPeople);
    getCategories().then(setCategories);
  }, []);

  const parseCurrencyInput = (raw: string) => {
    const cleaned = raw.trim().replace(/\s/g, "");
    if (!cleaned) return NaN;

    if (cleaned.includes(",") && cleaned.includes(".")) {
      const normalized = cleaned.replace(/\./g, "").replace(",", ".");
      return Number(normalized);
    }

    if (cleaned.includes(",")) {
      const normalized = cleaned.replace(",", ".");
      return Number(normalized);
    }

    return Number(cleaned);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const valorNumero = parseCurrencyInput(valor);

    if (!descricao || valorNumero <= 0 || !pessoaId || categoryId === "") {
      alert("Preencha todos os campos corretamente");
      return;
    }

    if (isMinor && tipoId === TransactionType.Receita) {
      alert("Pessoa menor de idade não pode cadastrar receitas");
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
          <label>Tipo</label>
          <select
            value={tipoId}
            onChange={e => setTipoId(Number(e.target.value) as TransactionType)}
          >
            <option value={TransactionType.Despesa}>Despesa</option>
            <option
              value={TransactionType.Receita}
              disabled={isMinor}
            >
              Receita
            </option>
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
