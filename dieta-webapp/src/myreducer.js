export const myreducer = (state, action) => {
    console.log(action);
  
    if (action.type === "CHECK WINNER") {
      state = { ...state };

      return state;
    }
  
    return state;
  };