import { useEffect, useState } from "react";
import { getPeople, deletePerson } from "../../api/people.api";
import type { Person } from "../../models/Person";
import PersonForm from "../People/PersonForm";

export default function PeoplePage() {
  const [people, setPeople] = useState<Person[]>([]);

  const loadPeople = async () => {
    const data = await getPeople();
    setPeople(data);
  };

  useEffect(() => {
    (async () => {
      const data = await getPeople();
      setPeople(data);
    })();
  }, []);

  const handleDelete = async (id: string) => {
    if (!confirm("Deseja realmente excluir essa pessoa?")) return;

    await deletePerson(id);
    loadPeople();
  };

  return (
    <div className="page-grid">
      <header className="page-header">
        <div>
          <h1 className="page-title">Pessoas</h1>
          <p className="page-subtitle">
            Cadastre quem participa das transações para ter relatórios mais
            completos.
          </p>
        </div>
      </header>

      <section className="form-card">
        <PersonForm onCreated={loadPeople} />
      </section>

      <section>
        <div className="page-section-title">Pessoas cadastradas</div>
        <div className="list-card">
          <ul>
            {people.map(p => (
              <li key={p.id} className="list-item">
                <div className="list-item-main">
                  <span className="list-item-title">{p.nome}</span>
                  <span className="list-item-sub">{p.idade} anos</span>
                </div>
                <div className="list-item-actions">
                  <button
                    className="btn btn-ghost-danger"
                    onClick={() => handleDelete(p.id)}
                  >
                    Excluir
                  </button>
                </div>
              </li>
            ))}
          </ul>

          {people.length === 0 && (
            <p className="muted-text">
              Nenhuma pessoa cadastrada ainda. Adicione alguém para começar.
            </p>
          )}
        </div>
      </section>
    </div>
  );
}
