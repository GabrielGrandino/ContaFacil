import { useEffect, useState } from "react";
import { getTransactions } from "../../api/transactions.api";
import type { Transaction } from "../../models/Transaction";
import TransactionForm from "./TransactionForm";
import { TransactionType } from "../../enums/TransactionType";

export default function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  const loadTransactions = async () => {
    const data = await getTransactions();
    setTransactions(data);
  };

  useEffect(() => {
    (async () => {
      const data = await getTransactions();
      setTransactions(data);
    })();
  }, []);

  const renderTipoPill = (tipo: string) => {
    if (tipo === TransactionType[TransactionType.Receita]) {
      return <span className="pill pill-income">Receita</span>;
    }
    if (tipo === TransactionType[TransactionType.Despesa]) {
      return <span className="pill pill-expense">Despesa</span>;
    }
    return <span className="pill pill-neutral">{tipo}</span>;
  };

  return (
    <div className="page-grid">
      <header className="page-header">
        <div>
          <h1 className="page-title">Transações</h1>
          <p className="page-subtitle">
            Cadastre novas movimentações e acompanhe sua lista de receitas e
            despesas.
          </p>
        </div>
      </header>

      <section className="form-card">
        <TransactionForm onCreated={loadTransactions} />
      </section>

      <section>
        <div className="page-section-title">Lista de transações</div>
        <div className="table-card">
          <div className="table-wrapper">
            <table>
              <thead>
                <tr>
                  <th>Descrição</th>
                  <th>Valor</th>
                  <th>Tipo</th>
                  <th>Categoria</th>
                  <th>Pessoa</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {transactions.map(t => (
                  <tr key={t.id}>
                    <td>{t.descricao}</td>
                    <td>R$ {t.valor.toFixed(2)}</td>
                    <td>{renderTipoPill(t.tipo)}</td>
                    <td>{t.categoria}</td>
                    <td>{t.pessoa}</td>
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
