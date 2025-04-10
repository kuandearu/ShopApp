import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';


const UpdateBrand: React.FC = () : JSX.Element => {
    const { id } = useParams();
    const [brand, setBrand] = useState({ name: "", image: "" });
    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBrand = async () => {
            try {
                const response = await axios.get(`${apiURL}/Brand/${id}`);
                setBrand(response.data);
            } catch (error) {
                console.error("Error fetching brand", error);
            }
        };
        fetchBrand();
    }, [id, apiURL]);

    const submitForm = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await axios.put(`${apiURL}/Brand/update/${id}`, brand);
            navigate(`/brand/all`);
        } catch (error) {
            console.error("Error updating brand", error);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setBrand({ ...brand, [e.target.name]: e.target.value });
    };

    return (
        <div className="p-8 max-w-md mx-auto">
            <h1 className="text-2xl font-bold text-center mb-4">Update Brand</h1>
            <form onSubmit={submitForm} className="space-y-4">
                <input type="text" name="name" value={brand.name} onChange={handleChange} placeholder="Enter Name" className="w-full p-2 border rounded" />
                <input type="text" name="image" value={brand.image} onChange={handleChange} placeholder="Enter Image URL" className="w-full p-2 border rounded" />
                <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded w-full">Update Brand</button>
            </form>
        </div>
    );
};

export default UpdateBrand;