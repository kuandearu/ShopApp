import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';

const UpdateCategory: React.FC = () : JSX.Element => {
    const { id } = useParams<{ id: string }>();
    const [category, setCategory] = useState({ name: '', image: '' });
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCategory = async () => {
            try {
                const response = await axios.get(`${apiURL}/Category/${id}`);
                setCategory(response.data);
            } catch (error) {
                console.error("Error fetching category", error);
            }
        };
        fetchCategory();
    }, [id, apiURL]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCategory({ ...category, [e.target.name]: e.target.value });
    };

    const submitForm = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await axios.put(`${apiURL}/Category/update/${id}`, category);
            navigate(`/category/all`);
        } catch (error) {
            console.error("Error updating category", error);
        }
    };

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold text-center mb-8">Update Category</h1>
            <form onSubmit={submitForm} className="max-w-lg mx-auto bg-white p-6 rounded shadow-md">
                <label className="block mb-2 font-semibold">Name</label>
                <input type="text" name="name" value={category.name} onChange={handleChange} placeholder='Enter Name' className="w-full p-2 border rounded mb-4" />
                <label className="block mb-2 font-semibold">Image URL</label>
                <input type="text" name="image" value={category.image} onChange={handleChange} placeholder='Enter Image URL' className="w-full p-2 border rounded mb-4" />
                <button type='submit' className="bg-blue-500 text-white px-4 py-2 rounded w-full">Update Category</button>
            </form>
        </div>
    );
};

export default UpdateCategory