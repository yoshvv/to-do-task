import React, { useEffect, useState } from 'react';
import { getAll, createItem, updateItem, deleteItem } from './api';

export default function App() {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [title, setTitle] = useState('');
  const [error, setError] = useState(null);

  const load = async () => {
    setLoading(true);
    try {
      const data = await getAll();
      setItems(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : String(err));
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { load(); }, []);

  const handleAdd = async (e) => {
    e.preventDefault();
    if (!title.trim()) return;
    try {
      const created = await createItem({ title: title.trim() });
      setItems(s => [created, ...s]);
      setTitle('');
    } catch (err) {
      setError(err instanceof Error ? err.message : String(err));
    }
  };

  const toggleComplete = async (item) => {
    try {
      const updated = { ...item, isCompleted: !item.isCompleted };
      const ok = await updateItem(updated.id, updated);
      if (ok) {
        setItems(s => s.map(i => i.id === item.id ? updated : i));
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : String(err));
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Delete this item?')) return;
    try {
      const ok = await deleteItem(id);
      if (ok) setItems(s => s.filter(i => i.id !== id));
    } catch (err) {
      setError(err instanceof Error ? err.message : String(err));
    }
  };

  return (
    <div style={{ fontFamily: 'sans-serif', padding: 20, maxWidth: 800, margin: '0 auto' }}>
      <h1>To-do</h1>
      <form onSubmit={handleAdd} style={{ marginBottom: 12 }}>
        <input
          value={title}
          onChange={e => setTitle(e.target.value)}
          placeholder="Add a new to-do"
          style={{ padding: 8, width: '70%', marginRight: 8 }}
        />
        <button type="submit" style={{ padding: '8px 12px' }}>Add</button>
      </form>

      {error && <div style={{ color: 'crimson', marginBottom: 12 }}>{error}</div>}

      {loading ? (
        <div>Loading...</div>
      ) : (
        <ul style={{ listStyle: 'none', padding: 0 }}>
          {items.length === 0 && <li>No items yet</li>}
          {items.map(item => (
            <li key={item.id} style={{ display: 'flex', alignItems: 'center', padding: '8px 0', borderBottom: '1px solid #eee' }}>
              <input type="checkbox" checked={item.isCompleted} onChange={() => toggleComplete(item)} />
              <div style={{ flex: 1, marginLeft: 12 }}>
                <div style={{ textDecoration: item.isCompleted ? 'line-through' : 'none', fontWeight: 500 }}>{item.title}</div>
                {item.description && <div style={{ fontSize: 12, color: '#666' }}>{item.description}</div>}
              </div>
              <button onClick={() => handleDelete(item.id)} style={{ marginLeft: 8 }}>Delete</button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
