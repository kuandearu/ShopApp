import axios from 'axios';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const AddCategory: React.FC = () : JSX.Element => {
    const [category, setCategory] = useState({ name: '', image: '' });
    const [error, setError] = useState<string | null>(null);
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCategory({ ...category, [e.target.name]: e.target.value });
    };

    const submitForm = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await axios.post(`${apiURL}/Category/create`, category);
            navigate(`/category/all`);
        } catch (error) {
            console.error("Error adding category", error);
            setError("Failed to add category");
        }
    };

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold text-center mb-8">Add Category</h1>
            {error && <p className="text-red-500 text-center">{error}</p>}
            <form onSubmit={submitForm} className="max-w-lg mx-auto bg-white p-6 rounded shadow-md">
                <label className="block mb-2 font-semibold">Name</label>
                <input type="text" name="name" value={category.name} onChange={handleChange} placeholder='Enter Name' className="w-full p-2 border rounded mb-4" />
                <label className="block mb-2 font-semibold">Image URL</label>
                <input type="text" name="image" value={category.image} onChange={handleChange} placeholder='Enter Image URL' className="w-full p-2 border rounded mb-4" />
                <button type='submit' className="bg-green-500 text-white px-4 py-2 rounded w-full">Add Category</button>
            </form>
        </div>
    );
};

export default AddCategory;