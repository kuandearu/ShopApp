import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const BrandList: React.FC = (): JSX.Element => {
    const [brands, setBrands] = useState<Brand[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    const handleAddButton = () => navigate(`/add-brand`);
    const handleUpdateButton = async (id: number) => navigate(`/update-brand/${id}`);
    const handleDeleteButton = async (id: number) => {
        try{
            const confirmDelete = window.confirm(`Do you want to delete this ${id}`);
            if(confirmDelete){
                await axios.delete(`${apiURL}/Brand/delete/${id}`);
                setBrands(brands.filter(brand => brand.id !== id));
            }
        }catch(error){
            console.error("Error delete brand", error);
            setError("Failed to delete product");
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get<Brand[]>(`${apiURL}/Brand/all`);
                setBrands(response.data);
            } catch (error) {
                setError("Failed to fetch brands");
                console.error("Error fetching brands", error);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, [apiURL]);

    if (loading) return <h1 className="text-center text-gray-500">Loading...</h1>;
    if (error) return <h1 className="text-center text-red-500">{error}</h1>;

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold text-center mb-8">Brand List</h1>
            <table className="min-w-full bg-white border border-gray-300 shadow-lg rounded-lg overflow-hidden">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="py-3 px-4 border-b text-left">ID</th>
                        <th className="py-3 px-4 border-b text-left">Name</th>
                        <th className="py-3 px-4 border-b text-left">Image</th>
                        <th className="py-3 px-4 border-b text-left">Action</th>
                    </tr>
                </thead>
                <tbody>
                    {brands.length > 0 ? (
                        brands.map(brand => (
                            <tr key={brand.id} className="hover:bg-gray-50 transition-colors">
                                <td className="py-3 px-4 border-b">{brand.id}</td>
                                <td className="py-3 px-4 border-b">{brand.name}</td>
                                <td className="py-3 px-4 border-b">
                                    <img src={brand.image} alt={brand.name} className="w-12 h-12 object-cover rounded" />
                                </td>
                                <td className="py-3 px-4 border-b">
                                    <button onClick={() => handleUpdateButton(brand.id)} className="bg-blue-500 text-white px-2 py-1 rounded mr-2">Update</button>
                                    <button onClick={() => handleDeleteButton(brand.id)} className="bg-red-500 text-white px-2 py-1 rounded">Delete</button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={4} className="py-6 text-center text-gray-500">No data found</td>
                        </tr>
                    )}
                </tbody>
            </table>
            <button onClick={handleAddButton} className="mt-4 bg-green-500 text-white px-4 py-2 rounded">Add Brand</button>
        </div>
    );
};

export default BrandList;
