
import "./CreateDiscussionForm.css";

import React, { useState } from "react";
import { createDiscussion } from "../../services/discussionService";

function CreateDiscussionForm({ onCreated }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setSuccess("");

    if (!title.trim()) {
      setError("Title is required.");
      return;
    }
    if (!description.trim()) {
      setError("Description is required.");
      return;
    }

    setIsSubmitting(true);

    try {
      const discussion = await createDiscussion({
        title: title.trim(),
        description: description.trim(),
      });

      setSuccess("Discussion created successfully.");
      setTitle("");
      setDescription("");

      // redirect na detalj diskusije:
      if (onCreated) {
        onCreated(discussion); // npr. navigate(`/discussions/${discussion.id}`)
      }
    } catch (err) {
      setError(err.message || "Something went wrong.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form className="create-discussion-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="title">Title *</label>
        <input
          id="title"
          type="text"
          maxLength={200}
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Edward ili Jacob?"
        />
      </div>

      <div className="form-group">
        <label htmlFor="description">Description *</label>
        <textarea
          id="description"
          rows={5}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Bez obzira podržavate li vampirski šarm Edwarda Cullena ili toplinu Jacobovog odnosa..."
        />
      </div>

      {/* Ovdje će kasnije doći Pick Books / Authors / Genre / Hashtags */}

      <button type="submit" disabled={isSubmitting}>
        {isSubmitting ? "Creating..." : "Finish"}
      </button>

      {error && <p className="error-text">{error}</p>}
      {success && <p className="success-text">{success}</p>}
    </form>
  );
}

export default CreateDiscussionForm;
