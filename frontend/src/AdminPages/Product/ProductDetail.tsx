import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const ProductDetail: React.FC = () : JSX.Element => {
  const { id } = useParams();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  const formatDate = (date: Date | string): string => {
    if (!date) return 'N/A';
    return new Date(date).toLocaleString();
  };

  useEffect(() => {
    const getDetailById = async () => {
      try {
        setLoading(true);
        const response = await axios.get(`${apiURL}/Product/${id}`);
        setProduct(response.data);
      } catch (error) {
        setError('Failed to fetch product details');
        console.error('Error fetching product details', error);
      } finally {
        setLoading(false);
      }
    };
    getDetailById();
  }, [apiURL, id]);

  if (loading) {
    return <div className="text-center text-gray-500">Loading Product details...</div>;
  }

  if (error) {
    return <div className="text-center text-red-500 font-bold">{error}</div>;
  }

  if (!product) {
    return <div className="text-center text-gray-500">No Product data found.</div>;
  }

  return (
    <div className="flex justify-center items-start min-h-screen pt-10">
      <div className="bg-white shadow-lg rounded-lg p-6 max-w-md w-full">
        <h1 className="text-2xl font-bold mb-4">Product Detail</h1>
        <p><strong>ID:</strong> {product.id}</p>
        <p><strong>Name:</strong> {product.name}</p>
        <p><strong>Price:</strong> ${product.price}</p>
        <p><strong>Old Price:</strong> ${product.oldPrice}</p>
        <img src={product.image} alt={product.name} className=" my-4 w-40 h-40 object-cover" />
        <p><strong>Description:</strong> {product.description}</p>
        <p><strong>Specification:</strong> {product.specification}</p>
        <p><strong>Buy Turn:</strong> {product.buyTurn}</p>
        <p><strong>Quantity:</strong> {product.quantity}</p>
        <p><strong>Brand ID:</strong> {product.brandId}</p>
        <p><strong>Category ID:</strong> {product.categoryId}</p>
        <p><strong>Created At:</strong> {formatDate(product.createdAt)}</p>
        <p><strong>Updated At:</strong> {formatDate(product.updatedAt)}</p>
        <button 
          onClick={() => navigate(`/product/all`)}
          className="mt-4 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
        >
          Return
        </button>
      </div>
    </div>
  );
};

export default ProductDetail;

// import axios from 'axios';
// import React, { useEffect, useState } from 'react'
// import { useNavigate, useParams } from 'react-router-dom';

// const ProductDetail : React.FC = () : JSX.Element => {
//   const{id} = useParams();
//   const[product, setProduct] = useState<Product>();
//   const[loading, setLoading] = useState<boolean>(true);
//   const[error, setError] = useState<string | null>(null);
//   const apiURL = import.meta.env.VITE_PUBLIC_URL;
//   const navigate = useNavigate();

//   const formatDate = (date: Date | string): string => {
//     if (!date) return 'N/A';
//     return new Date(date).toLocaleString();
//   };

//   useEffect(() => {
//     const getDetailById = async () => {
//       try{
//         setLoading(true);
//         const response = await axios.get(`${apiURL}/Product/${id}`);
//         setProduct(response.data);
//       }catch(error){
//         setError("Failed to fetch product id");
//         console.error("Error to fetch product id", error);
//       }finally{
//         setLoading(false);
//       }
//     };
//     getDetailById();
//   },[apiURL,id])  
//   if (loading) {
//     return <div>Loading Product details...</div>;
//   }

//   if (error) {
//     return <div className="text-red-500">{error}</div>;
//   }

//   if (!product) {
//     return <div>No Product data found.</div>;
//   }

//   return (
//     <div>
//       <h1>ProductDetail</h1>
      
//         <p><strong>ID:</strong> {product.id}</p>
//         <p><strong>name:</strong> {product.name   }</p>
//         <p><strong>price:</strong> {product.price}</p>
//         <p><strong>oldPrice:</strong> {product.oldPrice}</p>
//         <p><strong>image:</strong> {product.image}</p>
//         <p><strong>description:</strong> {product.description}</p>
//         <p><strong>specification:</strong> {product.specification}</p>
//         <p><strong>buyTurn:</strong> {product.buyTurn}</p>
//         <p><strong>quantity:</strong> {product.quantity}</p>
//         <p><strong>brandId:</strong> {product.brandId}</p>
//         <p><strong>categoryId:</strong> {product.categoryId}</p>
//         <p><strong>createdAt:</strong> {formatDate(product.createdAt)}</p>
//         <p><strong>updatedAt:</strong> {formatDate(product.updatedAt)}</p>

//         <button onClick={() => navigate(`/product/all`)}>return</button>
            
//     </div>
//   )
// }

// export default ProductDetail

{/* <div className="max-w-lg mx-auto p-6 bg-white shadow-lg rounded-lg">
      <h1 className="text-2xl font-bold text-center mb-4">Product Detail</h1>
      <form className="space-y-4">
        {Object.entries(product).map(([key, value]) => (
          <div key={key} className="flex flex-col">
            <label className="font-semibold text-gray-700 capitalize">
              {/* /([A-Z])/g → This is a regular expression that  Captures any uppercase letter (A-Z */}
              // {key.replace(/([A-Z])/g, ' $1')}: 
              {/* g → Global flag, meaning it will replace all occurrences of uppercase letters in the string.
              ' $1' → This means "replace each match with a space (' ') followed by the matched letter ($1)". */}
              // </label>
//             <input
//               type="text"
//               //If type of value is object(like Date) then formatDate(value) otherwise return value
//               value={typeof value === 'object' ? formatDate(value) : value}
//               readOnly
//               className="border rounded-md p-2 bg-gray-100 text-gray-700"
//             />
//           </div>
//         ))}
//         <button
//           type="button"
//           onClick={() => navigate('/product/all')}
//           className="w-full bg-blue-500 text-white py-2 rounded-md hover:bg-blue-600"
//         >
//           Return
//         </button>
//       </form>
//     </div>
//   );
// }; 