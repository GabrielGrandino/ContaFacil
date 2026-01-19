import { useEffect, useState } from "react";
import { getPeople } from "../../api/people.api";
import type { Person } from "../../models/Person";

export default function PeoplePage() {
  const [people, setPeople] = useState<Person[]>([]);

  useEffect(() => {
    getPeople().then(setPeople);
  }, []);

  return (
    <div>
      <h1>Pessoas</h1>

      <ul>
        {people.map(p => (
          <li key={p.id}>
            {p.nome} - {p.idade} anos
          </li>
        ))}
      </ul>
    </div>
  );
}
