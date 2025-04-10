import React from 'react';

interface Props {
  companyName : string,
  ticker: string,
  price: number,

};

const Card : React.FC<Props> = ({companyName, ticker, price}: Props) : JSX.Element => {
  return (
    <div className="max-w-md mx-auto bg-white rounded-2xl shadow-md overflow-hidden">
      <header className="p-4">
        {/* Image */}
        <img
          src="https://www.apple.com/ac/structured-data/images/knowledge_graph_logo.png?202305190221"
          alt="Apple Image"
          className="w-full h-48 object-contain"
        />
        {/* Title and Price */}
        <div className="flex justify-between items-center mt-4">
          <h2 className="text-xl font-semibold text-gray-800">{companyName} ({ticker})</h2>
          <h2 className="text-lg font-bold text-green-600">${price}</h2>
        </div>
        {/* Description */}
        <div className="mt-3">
          <h2 className="text-gray-600 text-sm">
            Lorem ipsum dolor sit, amet consectetur adipisicing elit. Obcaecati enim odit veritatis magnam atque
            doloribus nam cum expedita quaerat nobis.
          </h2>
        </div>
      </header>
    </div>
  );
};

export default Card;
