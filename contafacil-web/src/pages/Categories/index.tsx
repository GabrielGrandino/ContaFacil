import { useEffect, useState } from "react";
import { getCategories, deleteCategory } from "../../api/categories.api";
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

  const handleDelete = async (id: string) => {
    if (!confirm("Deseja realmente excluir essa categoria?")) return;

    await deleteCategory(id);
    loadCategories();
  };

  return (
    <div>
      <h1>Categorias</h1>

      <CategoryForm onCreated={loadCategories} />

      <hr />

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
              <td>
                <button onClick={() => handleDelete(c.id)}>
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
