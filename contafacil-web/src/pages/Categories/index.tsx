import { useEffect, useState } from "react";
import { getCategories } from "../../api/categories.api";
import type { Category } from "../../models/Category";
import CategoryForm from "./CategoryForm";

export default function CategoriesPage() {
  const [categories, setCategories] = useState<Category[]>([]);

  const loadCategories = async () => {
    const data = await getCategories();
    setCategories(data);
  };

  useEffect(() => {
    (async () => {
      const data = await getCategories();
      setCategories(data);
    })();
  }, []);

  return (
    <div className="page-grid">
      <header className="page-header">
        <div>
          <h1 className="page-title">Categorias</h1>
          <p className="page-subtitle">
            Organize suas transações em grupos para entender melhor seus gastos e
            receitas.
          </p>
        </div>
      </header>

      <section className="form-card">
        <CategoryForm onCreated={loadCategories} />
      </section>

      <section>
        <div className="page-section-title">Categorias cadastradas</div>
        <div className="table-card">
          <div className="table-wrapper">
            <table>
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>Finalidade</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {categories.map(c => (
                  <tr key={c.id}>
                    <td>{c.descricao}</td>
                    <td>{c.finalidade}</td>
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
