export interface Transaction {
    id: number;
    descricao: string;
    valor: number;
    tipo: "Despesa" | "Receita";
    categoria: string;
    pessoa: string;
  }
  