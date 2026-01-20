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

  const saldoCalculado =
    globalTotals?.totalReceitas != null && globalTotals?.totalDespesas != null
      ? globalTotals.totalReceitas - globalTotals.totalDespesas
      : globalTotals?.saldo ?? 0;

  return (
    <div className="page-grid">
      <header className="page-header">
        <div>
          <h1 className="page-title">Visão geral</h1>
          <p className="page-subtitle">
            Acompanhe rapidamente o saldo geral e os totais por pessoa.
          </p>
        </div>
        <span className="page-badge">Dashboard em tempo real</span>
      </header>

      {globalTotals && (
        <section>
          <div className="page-section-title">Totais gerais</div>
          <div className="cards-row">
            <TotalCard title="Receitas" value={globalTotals.totalReceitas} />
            <TotalCard title="Despesas" value={globalTotals.totalDespesas} />
            <TotalCard title="Saldo" value={saldoCalculado} />
          </div>
        </section>
      )}

      <section>
        <div className="page-section-title">Totais por pessoa</div>
        <div className="table-card">
          <div className="table-wrapper">
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
        </div>
      </section>
    </div>
  );
}
