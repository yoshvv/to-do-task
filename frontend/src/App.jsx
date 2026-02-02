import React, { useState } from 'react';

export default function App() {
  const [count, setCount] = useState(0);

  return (
    <div style={{ fontFamily: 'sans-serif', padding: 20 }}>
      <h1>Hello, world!</h1>
      <p>This is the initial React frontend for the to-do app.</p>
      <button onClick={() => setCount(c => c + 1)}>
        Clicked {count} {count === 1 ? 'time' : 'times'}
      </button>
    </div>
  );
}
