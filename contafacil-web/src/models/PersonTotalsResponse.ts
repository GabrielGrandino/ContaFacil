import type { PersonTotals } from "./PersonTotals";
import type { GlobalTotals } from "./GlobalTotals";

export interface PersonTotalsResponse {
  pessoas: PersonTotals[];
  totalGeral: GlobalTotals;
}
