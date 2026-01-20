import { useState } from "react";
import { createCategory } from "../../api/categories.api";

interface Props {
  onCreated: () => void;
}

export default function CategoryForm({ onCreated }: Props) {
  const [descricao, setDescricao] = useState("");
  const [purposeId, setPurposeId] = useState<number>(1);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!descricao.trim()) {
      alert("Nome da categoria é obrigatório");
      return;
    }

    try {
      setLoading(true);

      await createCategory({
        descricao,
        purposeId
      });

      setDescricao("");
      setPurposeId(1);
      onCreated();
    } catch {
      alert("Erro ao criar categoria");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Cadastrar Categoria</h2>

      <div className="form-grid">
        <div className="form-field">
          <label>Nome</label>
          <input
            type="text"
            value={descricao}
            placeholder="Ex.: Moradia, Lazer, Salário..."
            onChange={e => setDescricao(e.target.value)}
          />
        </div>

        <div className="form-field">
          <label>Finalidade</label>
          <select
            value={purposeId}
            onChange={e => setPurposeId(Number(e.target.value))}
          >
            <option value={1}>Despesa</option>
            <option value={2}>Receita</option>
            <option value={3}>Ambas</option>
          </select>
        </div>
      </div>

      <button type="submit" className="btn btn-primary" disabled={loading}>
        {loading ? "Salvando..." : "Salvar categoria"}      
      </button>
    </form>
  );
}
