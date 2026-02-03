const API_BASE = import.meta.env.VITE_API_BASE_URL?.replace(/\/$/, '') ?? '';

function apiUrl(path) {
  if (API_BASE) return `${API_BASE}${path.startsWith('/') ? '' : '/'}${path}`;
  return path.startsWith('/') ? path : `/${path}`;
}

async function request(path, options = {}) {
  const res = await fetch(apiUrl(path), {
    headers: { 'Content-Type': 'application/json' },
    ...options,
  });
  if (!res.ok) {
    const text = await res.text();
    throw new Error(`${res.status} ${res.statusText}: ${text}`);
  }
  
  if (res.status === 204) return null;
  return res.json();
}

export async function getAll() {
  return await request('/api/ToDo');
}

export async function getById(id) {
  return await request(`/api/ToDo/${id}`);
}

export async function createItem(item) {
  return await request('/api/ToDo', { method: 'POST', body: JSON.stringify(item) });
}

export async function updateItem(id, item) {
  await request(`/api/ToDo/${id}`, { method: 'PUT', body: JSON.stringify(item) });
  return true;
}

export async function deleteItem(id) {
  await request(`/api/ToDo/${id}`, { method: 'DELETE' });
  return true;
}

export default { getAll, getById, createItem, updateItem, deleteItem };
