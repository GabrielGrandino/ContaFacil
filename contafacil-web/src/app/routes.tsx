import { Routes, Route } from "react-router-dom";
import PeoplePage from "../pages/People/index";
import CategoriesPage from "../pages/Categories";
import TransactionsPage from "../pages/Transactions";
import Dashboard from "../pages/Dashboard";

export function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Dashboard />} />
      <Route path="/categories" element={<CategoriesPage />} />
      <Route path="/transactions" element={<TransactionsPage />} />
      <Route path="/persons" element={<PeoplePage />} />
    </Routes>
  );
}
