import { BrowserRouter, Routes, Route } from "react-router-dom";
import PeoplePage from "../pages/People/index";
import CategoriesPage from "../pages/Categories";
import TransactionsPage from "../pages/Transactions";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PeoplePage />} />
        <Route path="/categories" element={<CategoriesPage />} />
        <Route path="/transactions" element={<TransactionsPage />} />
      </Routes>
    </BrowserRouter>
  );
}
