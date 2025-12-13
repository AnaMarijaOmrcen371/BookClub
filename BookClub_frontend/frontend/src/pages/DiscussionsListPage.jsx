import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./DiscussionsListPage.css";



function DiscussionsListPage() {
  const [discussions, setDiscussions] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7022/api/discussions")
      .then((res) => res.json())
      .then((data) => setDiscussions(data))
      .catch((err) => console.error(err));
  }, []);

  return (
  <div className="discussions-page">
    <div className="discussions-container">
      <div className="discussions-header">
        <h1>Discussions</h1>
        <Link to="/create" className="create-link">
          + Create
        </Link>
      </div>

      <ul className="discussion-list">
        {discussions.map((d) => (
          <li key={d.id} className="discussion-item">
            <h3>{d.title}</h3>
            <small>{new Date(d.createdAt).toLocaleString()}</small>
          </li>
        ))}
      </ul>
    </div>
  </div>
);

}

export default DiscussionsListPage;
