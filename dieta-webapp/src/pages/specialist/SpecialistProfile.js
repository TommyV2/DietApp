import React from "react";
import { connect } from "react-redux";
import {NavigationBar} from "../../components/NavigationBar"
import {ProfileData} from "../../components/ProfileData"
import {url} from "../../commons/properties"
import {getCookie} from "../../commons/getCookie";

export class SpecialistProfile extends React.Component {
    constructor(props) {
        super(props);
        this.state = 
        {
            specialistId: getCookie("specialistId")
        };
       
        
      }
   

    async componentDidMount(){
        const response = await fetch(url+'/specialists/'+this.state.specialistId);
        const userData = await response.json();               
        this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData});
    }

    render(){   
            
        return(             
                <div> 
                <NavigationBar type="specialist"/>                     
                        <div>
                            {!this.props.userData ? (
                                <h3>loading...</h3>
                            ) : (
                                <div>
                                    <h3>Mój profil ( {this.props.userData.name} )</h3> 
                                    <ProfileData type = "specialist" data={this.props.userData}/>
                                </div> 
                            )}                           
                        </div>                                            
                </div>            
        );     
    }    
};

let mapStateToProps=(state) => {
    return {
       userData: state.userData 
    };    
};
   
SpecialistProfile = connect(mapStateToProps)(SpecialistProfile);
