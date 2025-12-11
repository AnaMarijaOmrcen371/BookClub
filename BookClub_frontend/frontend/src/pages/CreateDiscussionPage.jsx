
import React from "react";
import CreateDiscussionForm from "../components/discussions/CreateDiscussionForm";
import { useNavigate } from "react-router-dom";

function CreateDiscussionPage() {
  const navigate = useNavigate();

  const handleCreated = (discussion) => {
    // Nakon kreiranja prebaciti korisnika na detalje diskusije
    navigate(`/discussions/${discussion.id}`);
  };

  return (
    <div className="page create-discussion-page">
      <header className="page-header">
        <button onClick={() => navigate(-1)}>{"<"}</button>
        <h1>Create discussion</h1>
      </header>

      <CreateDiscussionForm onCreated={handleCreated} />
    </div>
  );
}

export default CreateDiscussionPage;
