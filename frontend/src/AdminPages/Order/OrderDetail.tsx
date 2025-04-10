import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const OrderDetail: React.FC = () => {
  const { id } = useParams();
  const [order, setOrder] = useState<Order | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  useEffect(() => {
    const fetchOrderDetail = async () => {
      try {
        setLoading(true);
        const response = await axios.get(`${apiURL}/Order/${id}`);
        setOrder(response.data);
      } catch (error) {
        setError('Failed to fetch order details');
        console.error('Error fetching order details', error);
      } finally {
        setLoading(false);
      }
    };
    fetchOrderDetail();
  }, [apiURL, id]);

  const updateQuantity = async (detailId: number, newQuantity: number) => {
    if (newQuantity < 1 || !order) return;
  
    const detail = order.orderDetails.find((d) => d.id === detailId);
    if (!detail) return;
  
    try {
      const payload = {
        orderId: order.id,
        productId: detail.productId,
        quantity: newQuantity,
      };
  
      const response = await axios.put(`${apiURL}/OrderDetail/update/${detailId}`, payload);
      console.log("API Response:", response.data);
  
      setOrder((prevOrder) => {
        if (!prevOrder) return prevOrder;
      
        const updatedDetails = prevOrder.orderDetails.map((d) =>
          d.id === detailId ? { ...d, quantity: newQuantity } : d
        );
      
        const newTotal = updatedDetails.reduce(
          (acc, d) => acc + d.price * d.quantity,
          0
        );
      
        return {
          ...prevOrder,
          orderDetails: updatedDetails,
          total: newTotal,
        };
      });
    } catch (error) {
      console.error('Failed to update quantity', error);
    }
  };

  if (loading) {
    return <div className="text-center text-gray-500">Loading Order details...</div>;
  }

  if (error) {
    return <div className="text-center text-red-500 font-bold">{error}</div>;
  }

  if (!order) {
    return <div className="text-center text-gray-500">No Order data found.</div>;
  }

  return (
    <div className="flex justify-center items-start min-h-screen pt-10">
      <div className="bg-white shadow-lg rounded-lg p-6 max-w-md w-full">
        <h1 className="text-2xl font-bold mb-4">Order Detail</h1>
        <p><strong>ID:</strong> {order.id}</p>
        <p><strong>User ID:</strong> {order.userId}</p>
        <p><strong>Status:</strong> {order.status}</p>
        <p><strong>Note:</strong> {order.note || 'N/A'}</p>
        <p><strong>Total:</strong> ${order.total.toFixed(2)}</p>
        <p><strong>Created At:</strong> {order.createdAt ? new Date(order.createdAt).toLocaleString() : 'N/A'}</p>
        <p><strong>Updated At:</strong> {order.updatedAt ? new Date(order.updatedAt).toLocaleString() : 'N/A'}</p>
        
        <h2 className="text-xl font-bold mt-4">Order Details</h2>
        {order.orderDetails?.length ? (
          <ul className="mt-2">
            {order.orderDetails.map((detail) => (
              <li key={detail.id} className="border-b py-2">
                <p><strong>Product ID:</strong> {detail.productId}</p>
                <p><strong>Price:</strong> ${detail.price}</p>
                <p><strong>Quantity:</strong> {detail.quantity}</p>
                <div className="flex gap-2 mt-2">
                  <button onClick={() => updateQuantity(detail.id, detail.quantity - 1)} className="px-2 py-1 bg-red-500 text-white rounded-lg">-</button>
                  <button onClick={() => updateQuantity(detail.id, detail.quantity + 1)} className="px-2 py-1 bg-green-500 text-white rounded-lg">+</button>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-500">No order details available.</p>
        )}
        
        <button 
          onClick={() => navigate(`/order/add-product/${id}`)}
          className="mt-4 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 w-full"
        >
          Add Product
        </button>
        <button 
          onClick={() => navigate(`/order/all`)}
          className="mt-4 px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600 w-full"
        >
          Return
        </button>
      </div>
    </div>
  );
};

export default OrderDetail;
