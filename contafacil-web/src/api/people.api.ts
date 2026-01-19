import { http } from "./http";
import type { Person } from "../models/Person";

export const getPeople = async (): Promise<Person[]> => {
  const response = await http.get<Person[]>("/people");
  return response.data;
};

export const createPerson = async (data: Omit<Person, "id">) => {
  await http.post("/people", data);
};

export const deletePerson = async (id: string) => {
  await http.delete(`/people/${id}`);
};

  