import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';


const UpdateProduct : React.FC = () : JSX.Element => {
    const{id} = useParams();
    const [product, setProduct] = useState({
        name: '',
        price: 0,
        oldPrice: 0,
        image: '',
        description: '',
        specification: '',
        buyTurn: 0,
        quantity: 0,
        brandId: 0,
        categoryId: 0,
        });

    const [brands, setBrands] = useState<Brand[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    const apiURL = import.meta.env.VITE_PUBLIC_URL;
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBrands = async () => {
        try {
            const response = await axios.get(`${apiURL}/Brand/all`);
            setBrands(response.data);
        } catch (error) {
            console.error('Error fetching brands', error);
        }
        };

        const fetchCategories = async () => {
        try {
            const response = await axios.get(`${apiURL}/Category/all`);
            setCategories(response.data);
        } catch (error) {
            console.error('Error fetching categories', error);
        }
        };

        const fetchProduct = async () => {
            try{
                const response = await axios.get(`${apiURL}/Product/${id}`);
                setProduct(response.data);
            }catch(error){
                console.error("Fail to fetch product", error);
            }
        }

        fetchBrands();
        fetchCategories();
        fetchProduct();
    }, [apiURL]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        setProduct({ ...product, [e.target.name]: e.target.value });
      };
    
      const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
          await axios.put(`${apiURL}/Product/update/${id}`, product);
          navigate('/product/all');
        } catch (error) {
          console.error('Failed to update product', error);
        }
      };

  return (
    <div className="max-w-2xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Update Product</h1>
      
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-gray-700 font-medium">Name</label>
          <input type="text" name="name" value={product.name} onChange={handleChange} placeholder="Product Name"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-gray-700 font-medium">Price</label>
            <input type="number" name="price" value={product.price} onChange={handleChange} placeholder="Price"
              className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
          </div>

          <div>
            <label className="block text-gray-700 font-medium">Old Price</label>
            <input type="number" name="oldPrice" value={product.oldPrice} onChange={handleChange} placeholder="Old Price"
              className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
          </div>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Image URL</label>
          <input type="text" name="image" value={product.image} onChange={handleChange} placeholder="Image URL"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"/>
        </div>

        <div>
          <label className="block text-gray-700 font-medium">Description</label>
          <textarea name="description" value={product.description} onChange={handleChange} placeholder="Description"
            className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none h-24 resize-none"></textarea>
          <label className="block text-gray-700 font-medium">Specification</label>
          <input type="text"
                 name='specification'
                 value={product.specification}
                 onChange={handleChange}
                 placeholder='Specification'
                 className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            />
            <label className="block text-gray-700 font-medium">Buy Turn</label>
          <input type="number"
                 name='buyTurn'
                 value={product.buyTurn}
                 onChange={handleChange}
                 placeholder='Buy Turn'
                 className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            />
            <label className="block text-gray-700 font-medium">Quantity</label>
          <input type="number"
                 name='quantity'
                 value={product.quantity}
                 onChange={handleChange}
                 placeholder='Quantity'
                 className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            />
          </div>
          

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-gray-700 font-medium">Brand</label>
            <select name="brandId" value={product.brandId} onChange={handleChange} required
              className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white">
              <option value="">Select Brand</option>
              {brands.map(brand => (
                <option key={brand.id} value={brand.id}>{brand.name}</option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-gray-700 font-medium">Category</label>
            <select name="categoryId" value={product.categoryId} onChange={handleChange} required
              className="w-full border rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white">
              <option value="">Select Category</option>
              {categories.map(category => (
                <option key={category.id} value={category.id}>{category.name}</option>
              ))}
            </select>
          </div>
        </div>

        <button type="submit"
          className="w-full bg-green-500 hover:bg-green-600 text-white font-medium py-2 rounded-lg transition duration-300">
          Update Product
        </button>
      </form>
    </div>
  )
}

export default UpdateProduct;