import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const UpdateAdmin: React.FC = () : JSX.Element => {
  const { id } = useParams<{ id: string }>();
  const [admin, setAdmin] = useState({
    email: '',
    password: '',
    name: '',
    avatar: '',
    phone: 0,
  });

  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAdmin = async () => {
      try {
        const response = await axios.get(`${apiURL}/Admin/${id}`);
        setAdmin(response.data);
      } catch (error) {
        console.error('Error fetching admin data', error);
      }
    };
    
    if (id) fetchAdmin();
  }, [id, apiURL]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setAdmin({ ...admin, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.put(`${apiURL}/Admin/update/${id}`, admin);
      navigate('/admin/all');
    } catch (error) {
      console.error('Failed to update admin', error);
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Update Admin</h1>
      
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-gray-700 font-medium">Email</label>
          <input type="email" name="email" value={admin.email} onChange={handleChange} placeholder="Admin Email"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none" required/>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Password</label>
          <input type="password" name="password" value={admin.password} onChange={handleChange} placeholder="Password"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none" required/>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Name</label>
          <input type="text" name="name" value={admin.name} onChange={handleChange} placeholder="Full Name"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none" required/>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Avatar URL</label>
          <input type="text" name="avatar" value={admin.avatar} onChange={handleChange} placeholder="Avatar URL"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Phone</label>
          <input type="number" name="phone" value={admin.phone} onChange={handleChange} placeholder="Phone Number"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none" required/>
        </div>

        <button type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 text-white font-medium py-2 rounded-lg transition duration-300">
          Update Admin
        </button>
      </form>
    </div>
  );
};

export default UpdateAdmin;
