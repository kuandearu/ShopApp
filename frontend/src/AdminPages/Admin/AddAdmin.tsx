import axios from 'axios';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const AddAdmin: React.FC = () : JSX.Element => {
  const [admin, setAdmin] = useState({
    email: '',
    password: '',
    name: '',
    role: 3,
    avatar: '',
    phone: 0,
  });

  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setAdmin({ ...admin, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post(`${apiURL}/Admin/create`, admin);
      navigate('/admin/all');
    } catch (error) {
      console.error('Failed to add admin', error);
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Add Admin</h1>
      
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

        {/* <div>
          <label className="block text-gray-700 font-medium">Role</label>
          <select name="role" value={admin.role} onChange={handleChange} required
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white">
            <option value="1">RegisteredUser</option>
            <option value="2">Moderator</option>
            <option value="3">Admin</option>
          </select>
        </div> */}

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
          className="w-full bg-green-500 hover:bg-green-600 text-white font-medium py-2 rounded-lg transition duration-300">
          Add Admin
        </button>
      </form>
    </div>
  );
};

export default AddAdmin;