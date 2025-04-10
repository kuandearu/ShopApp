import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

const AdminDetail: React.FC = () : JSX.Element => {
  const { id } = useParams<{ id: string }>();
  const [admin, setAdmin] = useState<Admin | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();
  const formatDate = (date: Date | string): string => {
    if (!date) return 'N/A';
    return new Date(date).toLocaleString();
  };
  useEffect(() => {
    const fetchAdmin = async () => {
      try {
        const response = await axios.get(`${apiURL}/Admin/${id}`);
        setAdmin(response.data);
      } catch (error) {
        console.error('Failed to fetch admin details', error);
      }
    };

    fetchAdmin();
  }, [id, apiURL]);

  if (!admin) {
    return <div className="text-center text-gray-500">Loading...</div>;
  }

  return (
    <div className="max-w-2xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Admin Details</h1>
      <div className="space-y-4">
        <p><strong>ID:</strong> {admin.id}</p>
        <p><strong>Email:</strong> {admin.email}</p>
        <p><strong>Name:</strong> {admin.name}</p>
        <p><strong>Avatar:</strong> {admin.avatar}</p>
        <p><strong>Phone:</strong> {admin.phone}</p>
        <p><strong>Role:</strong> {admin.role === 3 ? 'Admin' : admin.role === 2 ? 'Moderator' : 'Registered User'}</p>
        {admin.avatar && <img src={admin.avatar} alt="Avatar" className="w-32 h-32 rounded-full mx-auto" />}
        <p><strong>createdAt:</strong> {formatDate(admin.createdAt)}</p>
        <p><strong>createdAt:</strong> {formatDate(admin.updatedAt)}</p>
        
      </div>
      <div className="mt-6 flex justify-between">
        <button
          onClick={() => navigate(`/admin/update/${id}`)}
          className="bg-blue-500 hover:bg-blue-600 text-white font-medium py-2 px-4 rounded-lg transition duration-300">
          Edit Admin
        </button>
        <button
          onClick={() => navigate('/admin/all')}
          className="bg-gray-500 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-lg transition duration-300">
          Back to List
        </button>
      </div>
    </div>
  );
};

export default AdminDetail;
