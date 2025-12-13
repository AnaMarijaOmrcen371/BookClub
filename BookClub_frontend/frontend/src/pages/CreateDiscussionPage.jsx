
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
    <div className="create-discussion-page">
      <div className="create-discussion-layout">
        <h1>Create discussion</h1>
        <CreateDiscussionForm onCreated={handleCreated} />
      </div>
    </div>
  );
}

export default CreateDiscussionPage;
