import { useState } from "react";
import { createPerson } from "../../api/people.api";

interface Props {
  onCreated: () => void;
}

export default function PersonForm({ onCreated }: Props) {
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!nome.trim()) {
      alert("Nome é obrigatório");
      return;
    }

    const idadeNumero = Number(idade);
    if (isNaN(idadeNumero) || idadeNumero <= 0) {
      alert("Idade inválida");
      return;
    }

    try {
      setLoading(true);

      await createPerson({
        nome,
        idade: idadeNumero
      });

      setNome("");
      setIdade("");
      onCreated();
    } catch {
      alert("Erro ao criar pessoa");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Cadastrar Pessoa</h2>

      <div className="form-grid">
        <div className="form-field">
          <label>Nome</label>
          <input
            type="text"
            value={nome}
            placeholder="Nome completo"
            onChange={e => setNome(e.target.value)}
          />
        </div>

        <div className="form-field">
          <label>Idade</label>
          <input
            type="number"
            value={idade}
            onChange={e => setIdade(e.target.value)}
          />
        </div>
      </div>

      <button type="submit" className="btn btn-primary" disabled={loading}>
        {loading ? "Salvando..." : "Salvar pessoa"}
      </button>
    </form>
  );
}
