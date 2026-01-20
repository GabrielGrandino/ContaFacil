export const TransactionType = {
    Despesa: 1,
    Receita: 2
  } as const;
  
  export type TransactionType =
    typeof TransactionType[keyof typeof TransactionType];
  