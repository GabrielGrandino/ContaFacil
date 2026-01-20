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
  const [valor, setValor] = useState<number>(0);
  const [tipoId, setTipoId] = useState<TransactionType>(TransactionType.Despesa);
  const [pessoaId, setPessoaId] = useState("");
  const [categoryId, setCategoryId] = useState<number | "">("");

  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  useEffect(() => {
    getPeople().then(setPeople);
    getCategories().then(setCategories);
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!descricao || valor <= 0 || !pessoaId || categoryId === "") {
      alert("Preencha todos os campos corretamente");
      return;
    }

    await createTransaction({
      descricao,
      valor,
      tipoId,
      pessoaId,
      categoryId
    });

    setDescricao("");
    setValor(0);
    setTipoId(TransactionType.Despesa);
    setPessoaId("");
    setCategoryId("");

    onCreated();
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Nova Transação</h2>

      <input
        placeholder="Descrição"
        value={descricao}
        onChange={e => setDescricao(e.target.value)}
      />

      <input
        type="number"
        step="0.01"
        value={valor}
        onChange={e => setValor(Number(e.target.value))}
      />

      <select
        value={tipoId}
        onChange={e => setTipoId(Number(e.target.value) as TransactionType)}
      >
        <option value={TransactionType.Despesa}>Despesa</option>
        <option value={TransactionType.Receita}>Receita</option>
      </select>

      <select value={pessoaId} onChange={e => setPessoaId(e.target.value)}>
        <option value="">Pessoa</option>
        {people.map(p => (
          <option key={p.id} value={p.id}>
            {p.nome}
          </option>
        ))}
      </select>

      <select
        value={categoryId}
        onChange={e => setCategoryId(Number(e.target.value))}
      >
        <option value="">Categoria</option>
        {categories.map(c => (
          <option key={c.id} value={c.id}>
            {c.descricao}
          </option>
        ))}
      </select>

      <button type="submit">Salvar</button>
    </form>
  );
}
