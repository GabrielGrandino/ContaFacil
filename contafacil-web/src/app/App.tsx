import { BrowserRouter, NavLink } from "react-router-dom";
import { AppRoutes } from "./routes";

export default function App() {
  return (
    <BrowserRouter>
      <div className="app-shell">
        <div className="app-layout">
          <aside className="sidebar">
            <div className="app-logo">
              <div className="logo-mark">CF</div>
              <div>
                <div className="logo-text-main">ContaFácil</div>
                <div className="logo-text-sub">Controle financeiro simples</div>
              </div>
            </div>

            <nav className="nav">
              <NavLink
                to="/"
                end
                className={({ isActive }) =>
                  `nav-link ${isActive ? "active" : ""}`
                }
              >
                <span className="nav-link-indicator" />
                <span>Dashboard</span>
              </NavLink>

              <NavLink
                to="/persons"
                className={({ isActive }) =>
                  `nav-link ${isActive ? "active" : ""}`
                }
              >
                <span className="nav-link-indicator" />
                <span>Pessoas</span>
              </NavLink>

              <NavLink
                to="/categories"
                className={({ isActive }) =>
                  `nav-link ${isActive ? "active" : ""}`
                }
              >
                <span className="nav-link-indicator" />
                <span>Categorias</span>
              </NavLink>

              <NavLink
                to="/transactions"
                className={({ isActive }) =>
                  `nav-link ${isActive ? "active" : ""}`
                }
              >
                <span className="nav-link-indicator" />
                <span>Transações</span>
              </NavLink>

            </nav>
          </aside>

          <main className="app-main">
            <AppRoutes />
          </main>
        </div>
      </div>
    </BrowserRouter>
  );
}
