import React from 'react';

class Tables extends React.Component {

    constructor(props) {
        super(props);
    }

    renderTableData() {
        return this.props.tables.map((table) => {
            return (
                <tr key={table.name} onClick={() => this.props.loadColumns(table)}>
                    <td>{table.schema}.{table.name}</td>
                </tr>
            )
        })
    }

    render() {
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Tables</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderTableData()}
                </tbody>
            </table>
        );
    }
}

export default Tables;
