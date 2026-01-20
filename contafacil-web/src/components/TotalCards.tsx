interface Props {
    title: string;
    value?: number;
  }
  
  export function TotalCard({ title, value = 0 }: Props) {
    const isNegative = value < 0;
  
    return (
      <div className={`total-card ${isNegative ? 'negative' : 'positive'}`}>
        <h3>{title}</h3>
        <strong>
          R$ {value.toFixed(2)}
        </strong>
      </div>
    );
  }
  