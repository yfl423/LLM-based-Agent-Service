function sendMessage() {
  const input = document.getElementById("user_input");
  const chatBox = document.getElementById("chat_box");
  const message = input.value.trim();

  if (message === "") return;

  // User message
  const userDiv = document.createElement("div");
  userDiv.className = "message user_message";
  userDiv.textContent = message;
  chatBox.appendChild(userDiv);

  // AI response 
  // const AIDiv = document.createElement("div");
  // AIDiv.className = "message AI_message";
  // AIDiv.textContent = getBotResponse(message);
  // chatBox.appendChild(AIDiv);

  input.value = "";
  // chatBox.scrollTop = chatBox.scrollHeight;
  // Call backend API
  fetch(`http://localhost:5120/api/client/infoPass?sessionID=test-session&message=${encodeURIComponent(message)}`)
    .then(response => response.text())
    .then(botReply => {
      const AIDiv = document.createElement("div");
      AIDiv.className = "message AI_message";
      AIDiv.textContent = botReply;
      chatBox.appendChild(AIDiv);
      chatBox.scrollTop = chatBox.scrollHeight;
  })
    .catch(error => {
      console.error("API call failed:", error);
      const errorDiv = document.createElement("div");
      errorDiv.className = "message AI_message";
      errorDiv.textContent = "Sorry, something went wrong.";
      chatBox.appendChild(errorDiv);
  });

  }

  
  function getBotResponse(msg) {
    // Replace with real backend/API logic
    if (msg.toLowerCase().includes("recommend")) {
      return "I recommend the Unlimited Plan for $45/month with 25GB of data.";
    } else if (msg.toLowerCase().includes("change")) {
      return "Sure, I can help you change your plan. What would you like to switch to?";
    } else {
      return "I'm here to help! You can ask about phone plans, pricing, or switching.";
    }
  }

  function showInitialAIMessage() {
    const chatBox = document.getElementById("chat_box");
    const AIDiv = document.createElement("div");
    AIDiv.className = "message AI_message";
    AIDiv.textContent = "Hi! I'm your assistant. How can I help you today?";
    chatBox.appendChild(AIDiv);
    chatBox.scrollTop = chatBox.scrollHeight;
  }
  
  
  const inputField = document.getElementById("user_input");
  inputField.addEventListener("keypress", function(event) {
    if (event.key === "Enter") {
      event.preventDefault();
      sendMessage();
    }
  });

  window.onload = function () {
    showInitialAIMessage();
  };
  