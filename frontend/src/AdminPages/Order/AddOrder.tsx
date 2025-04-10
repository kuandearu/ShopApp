import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';



const AddOrder : React.FC = () : JSX.Element => {

  const[order, setOrder] = useState({
    userId: 0,
    status: 0,
    note: "",
    total: 0,
  });

  const[users, setUsers] = useState<User[]>([]);

  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsers = async () => {
      try{
        const response = await axios.get(`${apiURL}/User/all`);
        setUsers(response.data);
      }catch(error){
        console.error("Failed to fetch users");
      }
    }
    fetchUsers();
  },[apiURL])

  const handleChange = async (e : React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setOrder({...order, [e.target.name] : e.target.value});
  }

  const handleSubmit = async (e : React.FormEvent) => {
    e.preventDefault();
    try{
      await axios.post(`${apiURL}/Order/create`, order);
      navigate(`/order/all`);
    }catch(error){
      console.error('Failed to add order', error);
    }
  }

  return (
    <div className="max-w-2xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Add Order</h1>
      
      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-gray-700 font-medium">UserID</label>
            <select name="userId" value={order.userId} onChange={handleChange} required
              className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white">
              <option value="">Select User</option>
              {users.map(user => (
                <option key={user.id} value={user.id}>{user.name}</option>
              ))}
            </select>
          </div>
        </div>
        <div>
          <label className="block text-gray-700 font-medium">Note</label>
          <input type="text" name="note" value={order.note} onChange={handleChange} placeholder="Note"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
        </div>

        <button type="submit"
          className="w-full bg-green-500 hover:bg-green-600 text-white font-medium py-2 rounded-lg transition duration-300">
          Add Product
        </button>
      </form>
    </div>
  )
}

export default AddOrder