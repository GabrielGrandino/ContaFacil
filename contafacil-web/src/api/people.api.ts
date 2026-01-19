import { http } from "./http";
import type { Person } from "../models/Person";

export const getPeople = async (): Promise<Person[]> => {
  const response = await http.get<Person[]>("/persons");
  return response.data;
};

export const createPerson = async (data: Omit<Person, "id">) => {
  await http.post("/persons", data);
};

export const deletePerson = async (id: string) => {
  await http.delete(`/persons/${id}`);
};

  