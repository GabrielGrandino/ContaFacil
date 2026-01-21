import { http } from "./http";

export interface CreateCategoryRequest {
  descricao: string;
  purposeId: number;
}

export const getCategories = async () => {
  const response = await http.get("api/categories");
  return response.data;
};

export const createCategory = async (data: CreateCategoryRequest) => {
  await http.post("api/categories", data);
};
