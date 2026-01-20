import { useEffect, useState } from "react";
import { getPersonTotals } from "../../api/reports.api";
import type { PersonTotals } from "../../models/PersonTotals";
import type { GlobalTotals } from "../../models/GlobalTotals";
import { TotalCard } from "../../components/TotalCards";

export default function Dashboard() {
  const [peopleTotals, setPeopleTotals] = useState<PersonTotals[]>([]);
  const [globalTotals, setGlobalTotals] = useState<GlobalTotals | null>(null);

  useEffect(() => {
    getPersonTotals().then(response => {
      setPeopleTotals(response.pessoas);
      setGlobalTotals(response.totalGeral);
    });
  }, []);

  return (
    <div>
      <h1>Dashboard</h1>

      {globalTotals && (
        <div style={{ display: "flex", gap: 16, marginBottom: 24 }}>
          <TotalCard title="Receitas" value={globalTotals.totalReceitas} />
          <TotalCard title="Despesas" value={globalTotals.totalDespesas} />
          <TotalCard title="Saldo" value={globalTotals.saldo} />
        </div>
      )}

      <h2>Totais por Pessoa</h2>

      <table>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {peopleTotals.map(p => (
            <tr key={p.pessoaId}>
              <td>{p.nome}</td>
              <td>R$ {p.totalReceitas.toFixed(2)}</td>
              <td>R$ {p.totalDespesas.toFixed(2)}</td>
              <td>R$ {p.saldo.toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
