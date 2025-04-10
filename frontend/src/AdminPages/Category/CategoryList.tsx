import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const CategoryList: React.FC = (): JSX.Element => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    const handleAddButton = () => navigate(`/add-category`);
    const handleUpdateButton = async (id: number) => navigate(`/update-category/${id}`);
    const handleDeleteButton = async (id: number) => {
        try{
            const confirmDelete = window.confirm(`Do you want to delete this ${id}`);
            if(confirmDelete){
                await axios.delete(`${apiURL}/Category/delete/${id}`);
                setCategories(categories.filter(category => category.id !== id));
            }
        }catch(error){
            console.error("Error delete Category", error);
            setError("Failed to delete Category");
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get<Category[]>(`${apiURL}/Category/all`);
                setCategories(response.data);
            } catch (error) {
                setError("Failed to fetch categories");
                console.error("Error fetching categories", error);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, [apiURL]);



    if (loading) {
        return <h1 className="text-center text-lg font-semibold">Loading...</h1>;
    }
    if (error) {
        return <h1 className="text-center text-red-500 font-semibold">{error}</h1>;
    }

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold text-center mb-8">Category List</h1>
            <table className="min-w-full bg-white border border-gray-300 shadow-lg rounded-lg overflow-hidden">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="py-3 px-4 border-b text-left">ID</th>
                        <th className="py-3 px-4 border-b text-left">Name</th>
                        <th className="py-3 px-4 border-b text-left">Image</th>
                        <th className="py-3 px-4 border-b text-left">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {categories.length > 0 ? (
                        categories.map((category) => (
                            <tr key={category.id} className="hover:bg-gray-50 transition-colors">
                                <td className="py-3 px-4 border-b">{category.id}</td>
                                <td className="py-3 px-4 border-b">{category.name}</td>
                                <td className="py-3 px-4 border-b">
                                    <img
                                        src={category.image}
                                        alt={`Category: ${category.name}`}
                                        className="w-12 h-12 object-cover rounded"
                                    />
                                </td>
                                <td className="py-3 px-4 border-b">
                                    <button 
                                        onClick={() => handleUpdateButton(category.id)} 
                                        className="bg-blue-500 text-white px-2 py-1 rounded mr-2 cursor-pointer">
                                        Update
                                    </button>
                                    <button 
                                        onClick={() => handleDeleteButton(category.id)} 
                                        className="bg-red-500 text-white px-2 py-1 rounded cursor-pointer">
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={4} className="py-6 text-center text-gray-500">
                                No categories found.
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
            <button 
                onClick={handleAddButton} 
                className='mt-4 bg-green-500 text-white px-4 py-2 rounded cursor-pointer'>
                Add Category
            </button>
        </div>
    );
}

export default CategoryList;
