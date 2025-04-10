import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const AdminList: React.FC = () : JSX.Element => {
  const [admins, setAdmins] = useState<Admin[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [filteredResults, setFilteredResults] = useState<Admin[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const apiURL = import.meta.env.VITE_PUBLIC_URL;
  const navigate = useNavigate();

  const handleAddButton = () => navigate(`/add-admin`);
  const handleUpdateButton = (id: number) => navigate(`/update-admin/${id}`);
  const handleDetailButton = (id: number) => navigate(`/admin/${id}`);
  const handleDeleteButton = async (id: number) => {
    try {
      const confirmDelete = window.confirm(`Do you want to delete admin: ${id}`);
      if (confirmDelete) {
        await axios.delete(`${apiURL}/Admin/delete/${id}`);
        setAdmins(admins.filter(admin => admin.id !== id));
      }
    } catch (error) {
      console.error("Failed to delete admin", error);
      setError("Failed to delete admin");
    }
  };

  useEffect(() => {
    async function fetchAdmins() {
      try {
        const response = await axios.get<Admin[]>(`${apiURL}/Admin/all`);
        setAdmins(response.data);
        setFilteredResults(response.data);
      } catch (error) {
        console.error("Fail to fetch admins", error);
        setError("Fail to load admins");
      } finally {
        setLoading(false);
      }
    }
    fetchAdmins();
  }, [apiURL]);

  useEffect(() => {
    const filtered = admins.filter(admin => 
      admin.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredResults(filtered);
  }, [searchTerm, admins]);

  if (loading) return <div className="text-center text-lg font-semibold">Loading...</div>;
  if (error) return <div className="text-center text-lg text-red-500">{error}</div>;

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold text-center mb-8">Admin List</h1>
      <input 
        type="text" 
        placeholder="Search by name..." 
        className="mb-4 p-2 border border-gray-300 rounded w-full"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <table className="min-w-full bg-white border border-gray-300 shadow-lg rounded-lg overflow-hidden">
        <thead className="bg-gray-100">
          <tr>
            <th className="py-3 px-4 border-b text-left">ID</th>
            <th className="py-3 px-4 border-b text-left">Name</th>
            <th className="py-3 px-4 border-b text-left">Email</th>
            <th className="py-3 px-4 border-b text-left">Role</th>
            <th className="py-3 px-4 border-b text-left">Avatar</th>
            <th className="py-3 px-4 border-b text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredResults.length > 0 ? (
            filteredResults.map((admin) => (
              <tr key={admin.id} className="hover:bg-gray-50 transition-colors">
                <td className="py-3 px-4 border-b">{admin.id}</td>
                <td className="py-3 px-4 border-b">{admin.name}</td>
                <td className="py-3 px-4 border-b">{admin.email}</td>
                <td className="py-3 px-4 border-b">{admin.role}</td>
                <td className="py-3 px-4 border-b">
                  <img src={admin.avatar} alt={admin.name} className="w-12 h-12 object-cover rounded" />
                </td>
                <td className="py-3 px-4 border-b">
                  <button onClick={() => handleUpdateButton(admin.id)} className="bg-blue-500 text-white px-2 py-1 rounded mr-2">Update</button>
                  <button onClick={() => handleDeleteButton(admin.id)} className="bg-red-500 text-white px-2 py-1 rounded mr-2">Delete</button>
                  <button onClick={() => handleDetailButton(admin.id)} className="bg-green-500 text-white px-2 py-1 rounded">View Detail</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={6} className="py-6 text-center text-gray-500">No admins available.</td>
            </tr>
          )}
        </tbody>
      </table>
      <button onClick={handleAddButton} className='mt-4 bg-green-500 text-white px-4 py-2 rounded'>Add Admin</button>
    </div>
  );
};

export default AdminList;
