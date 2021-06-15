
export const myreducer=(state, action)=>{

  if(action.type==="LOAD_USER_DATA"){
      state = { ...state };  
      let userData = action.userData
      state.userData = userData;  
      return state;
    }
  if(action.type==="LOAD_USER_COLLABORATIONS"){
      state = { ...state };  
      let userCollaborations = action.userCollaborations
      state.userCollaborations = userCollaborations;  
      return state;
    }
  if(action.type==="LOAD_USER_INVITATIONS"){
      state = { ...state };  
      let userInvitations = action.userInvitations 
      state.userInvitations  = userInvitations ;  
      return state;
    }
  if(action.type==="LOAD_USER_LIST"){
      state = { ...state };  
      let userList = action.userList 
      state.userList  = userList ;  
      return state;
    }
  if(action.type==="LOAD_NUTRIENTS"){
      state = { ...state };  
      let availableNutrients = action.availableNutrients 
      state.availableNutrients  = availableNutrients ;  
      return state;
    }
  if(action.type==="LOAD_AVAILABLE_PRODUCTS"){
      state = { ...state };  
      let availableProducts = action.availableProducts
      state.availableProducts  = availableProducts;  
      return state;
    }
    if(action.type==="LOAD_AVAILABLE_RECIPES"){
      state = { ...state };  
      let availableRecipes = action.availableRecipes
      state.availableRecipes  = availableRecipes;  
      return state;
    }
  if(action.type==="LOAD_USER_RECOMMENDATIONS"){
      state = { ...state };  
      let recommendations = action.recommendations
      state.recommendations  = recommendations;  
      return state;
    }
  if(action.type==="LOAD_USER_DIET_PLANS"){
      state = { ...state };  
      let dietPlans = action.dietPlans
      state.dietPlans  = dietPlans;  
      return state;
    }

    return state;
      
}
