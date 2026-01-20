import { http } from "./http";

export interface CreateTransactionRequest {
    descricao: string;
    valor: number;
    tipoId: number;
    categoryId: number;
    pessoaId: string;
  }

export const getTransactions = async () => {
  const response = await http.get("api/transactions");
  return response.data;
};

export const createTransaction = async (data: CreateTransactionRequest) => {
    await http.post("api/transactions", data);
  };

export const deleteTransaction = async (id: number) => {
  await http.delete(`api/transactions/${id}`);
};
