export interface Category {
    id: string;
    descricao: string;
    purposeId: number; // 1 despesa | 2 receita | 3 ambas
    finalidade: "despesa" | "receita" | "ambas";
  }
  