import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

interface Product {
  id: number;
  name: string;
  price: number;
}

const AddProductToOrder: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProductId, setSelectedProductId] = useState<number | null>(null);
  const [quantity, setQuantity] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await axios.get(`${apiURL}/Product/all`);
        setProducts(response.data);
      } catch (err) {
        setError('Failed to load products');
      }
    };
    fetchProducts();
  }, [apiURL]);

  const handleAddProduct = async () => {
    if (!selectedProductId) return;
    setLoading(true);
    try {
      await axios.post(`${apiURL}/OrderDetail/create`, {
        orderId: Number(id),
        productId: selectedProductId,
        quantity,
        price: products.find(p => p.id === selectedProductId)?.price || 0,
      });
      navigate(`/order/${id}`);
    } catch (err) {
      setError('Failed to add product to order');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex justify-center items-start min-h-screen pt-10">
      <div className="bg-white shadow-lg rounded-lg p-6 max-w-md w-full">
        <h1 className="text-2xl font-bold mb-4">Add Product to Order</h1>
        {error && <p className="text-red-500">{error}</p>}
        <label className="block mb-2">Select Product:</label>
        <select
          value={selectedProductId ?? ''}
          onChange={(e) => setSelectedProductId(Number(e.target.value))}
          className="border rounded p-2 w-full"
        >
          <option value="">-- Select Product --</option>
          {products.map(product => (
            <option key={product.id} value={product.id}>{product.name} - ${product.price}</option>
          ))}
        </select>

        <div className="flex items-center mt-4">
          <button
            className="px-3 py-1 bg-gray-300 text-black rounded-l"
            onClick={() => setQuantity(q => Math.max(1, q - 1))}
          >
            -
          </button>
          <input
            type="number"
            value={quantity}
            onChange={(e) => setQuantity(Math.max(1, Number(e.target.value)))}
            className="w-12 text-center border-t border-b"
          />
          <button
            className="px-3 py-1 bg-gray-300 text-black rounded-r"
            onClick={() => setQuantity(q => q + 1)}
          >
            +
          </button>
        </div>

        <button
          onClick={handleAddProduct}
          className="mt-4 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 w-full"
          disabled={!selectedProductId || loading}
        >
          {loading ? 'Adding...' : 'Add Product'}
        </button>
        <button
          onClick={() => navigate(`/order/${id}`)}
          className="mt-2 px-4 py-2 bg-gray-500 text-white rounded-lg hover:bg-gray-600 w-full"
        >
          Cancel
        </button>
      </div>
    </div>
  );
};

export default AddProductToOrder;
