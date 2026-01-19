import { BrowserRouter, Routes, Route } from "react-router-dom";
import PeoplePage from "../pages/People/index";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PeoplePage />} />
      </Routes>
    </BrowserRouter>
  );
}
