import React from "react";
import { connect } from "react-redux";
import {NavigationBar} from "../../components/NavigationBar";
import {ProfileData} from "../../components/ProfileData";
import {url} from "../../commons/properties";

export class ClientProfile extends React.Component {

    async componentDidMount(){
        const response = await fetch(url+'/client/1');
        const userData = await response.json();               
        this.props.dispatch({ type: "LOAD_USER_DATA", userData: userData});
    }

    render() {

        return (
            <div>
                <NavigationBar type="client" />
                <div>
                    {!this.props.userData ? (
                        <h3>loading...</h3>
                    ) : (
                        <div>
                            <h3>Mój profil ( {this.props.userData.name} )</h3>
                            <ProfileData type="client" data={this.props.userData} />
                        </div>
                    )}
                </div>
            </div>
        );
    }
}


let mapStateToProps=(state) => {
    return {
       userData: state.userData 
    };    
};
   
ClientProfile = connect(mapStateToProps)(ClientProfile);