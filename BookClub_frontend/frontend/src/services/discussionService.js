// src/services/discussionService.js

const API_BASE_URL = "https://localhost:7022"; 

export async function createDiscussion({ title, description }) {
  const response = await fetch(`${API_BASE_URL}/api/discussions`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ title, description }),
  });

  if (!response.ok) {
    // pokupi tekst greške ako ga backend šalje
    const errorText = await response.text();
    throw new Error(errorText || "Failed to create discussion.");
  }

  // vraća DiscussionDto iz backend-a
  return await response.json();
}
