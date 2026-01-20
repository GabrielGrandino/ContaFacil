import { http } from "./http";
import type { PersonTotalsResponse } from "../models/PersonTotalsResponse";

export const getPersonTotals = async (): Promise<PersonTotalsResponse> => {
  const response = await http.get("api/reports/totais");
  return response.data;
};
