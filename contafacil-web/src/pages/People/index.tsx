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
    <div>
      <h1>Pessoas</h1>

      <PersonForm onCreated={loadPeople} />

      <hr />

      <ul>
        {people.map(p => (
          <li key={p.id}>
            {p.nome} - {p.idade} anos
            <button onClick={() => handleDelete(p.id)}>
              Excluir
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
