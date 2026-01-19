import { http } from "./http";
import type { Person } from "../models/Person";

export const getPeople = async (): Promise<Person[]> => {
  const response = await http.get<Person[]>("api/persons");
  return response.data;
};

export const createPerson = async (data: Omit<Person, "id">) => {
  await http.post("api/persons", data);
};

export const deletePerson = async (id: string) => {
  await http.delete(`api/persons/${id}`);
};

  