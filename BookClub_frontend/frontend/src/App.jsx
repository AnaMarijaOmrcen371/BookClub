import { BrowserRouter, Routes, Route } from "react-router-dom";

import CreateDiscussionPage from "./pages/CreateDiscussionPage";
import DiscussionsListPage from "./pages/DiscussionsListPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
          <Route path="/create" element={<CreateDiscussionPage />} />
        <Route path="/" element={<DiscussionsListPage />} />
      
      </Routes>
    </BrowserRouter>
  );
}

export default App;
