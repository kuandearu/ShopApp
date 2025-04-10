import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const OrderList: React.FC = (): JSX.Element => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [filteredResults, setFilteredResults] = useState<Order[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  const handleAddButton = () => {
    navigate(`/add-order`);
  };

  // const handleUpdateButton = (id: number) => {
  //   navigate(`/update-order/${id}`);
  // };

  const handleDetailButton = (id: number) => {
    navigate(`/order/${id}`);
  };

  const handleDeleteButton = async (id: number) => {
    try {
      const confirmDelete = window.confirm(`Do you want to delete order: ${id}`);
      if (confirmDelete) {
        await axios.delete(`${apiURL}/Order/delete/${id}`);
        setOrders(orders.filter(order => order.id !== id));
      }
    } catch (error) {
      console.error("Failed to delete order", error);
      setError("Failed to delete order");
    }
  };

  useEffect(() => {
    async function fetchAll() {
      try {
        const response = await axios.get<Order[]>(`${apiURL}/Order/all`);
        setOrders(response.data);
        setFilteredResults(response.data);
      } catch (error) {
        console.error("Fail to fetch data", error);
        setError("Fail to load orders");
      } finally {
        setLoading(false);
      }
    }
    fetchAll();
  }, [apiURL]);

  useEffect(() => {
    const filtered = orders.filter(order => 
      order.note.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredResults(filtered);
  }, [searchTerm, orders]);

  if (loading) {
    return <div className="text-center text-lg font-semibold">Loading...</div>;
  }

  if (error) {
    return <div className="text-center text-lg text-red-500">{error}</div>;
  }

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold text-center mb-8">Order List</h1>
      <input 
        type="text" 
        placeholder="Search by note..." 
        className="mb-4 p-2 border border-gray-300 rounded w-full"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <table className="min-w-full bg-white border border-gray-300 shadow-lg rounded-lg overflow-hidden">
        <thead className="bg-gray-100">
          <tr>
            <th className="py-3 px-4 border-b text-left">ID</th>
            <th className="py-3 px-4 border-b text-left">User ID</th>
            <th className="py-3 px-4 border-b text-left">Status</th>
            <th className="py-3 px-4 border-b text-left">Note</th>
            <th className="py-3 px-4 border-b text-left">Total</th>
            {/* <th className="py-3 px-4 border-b text-left">Created At</th>
            <th className="py-3 px-4 border-b text-left">Updated At</th> */}
            <th className="py-3 px-4 border-b text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredResults.length > 0 ? (
            filteredResults.map((order) => (
              <tr key={order.id} className="hover:bg-gray-50 transition-colors">
                <td className="py-3 px-4 border-b">{order.id}</td>
                <td className="py-3 px-4 border-b">{order.userId}</td>
                <td className="py-3 px-4 border-b">{order.status}</td>
                <td className="py-3 px-4 border-b">{order.note}</td>
                <td className="py-3 px-4 border-b">${order.total.toFixed(2)}</td>
                {/* <td className="py-3 px-4 border-b">{new Date(order.createdAt).toLocaleString()}</td>
                <td className="py-3 px-4 border-b">{new Date(order.updatedAt).toLocaleString()}</td> */}
                <td>
                  {/* <button onClick={() => handleUpdateButton(order.id)} className="bg-blue-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">Update</button> */}
                  <button onClick={() => handleDeleteButton(order.id)} className="bg-red-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">Delete</button>
                  <button onClick={() => handleDetailButton(order.id)} className="bg-green-500 text-white px-2 py-1 rounded cursor-pointer">View Detail</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={8} className="py-6 text-center text-gray-500">
                There are no orders available.
              </td>
            </tr>
          )}
        </tbody>
      </table>
      <button onClick={handleAddButton} className='bg-green-500 text-white px-2 py-2 rounded mt-4 cursor-pointer'>Add Order</button>
    </div>
  );
};

export default OrderList;
